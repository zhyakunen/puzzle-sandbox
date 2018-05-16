using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleContainer : MonoBehaviour,ICommonTouchListener {

    [Range(1, 20)] public int width = 5, height = 10;
    public float groundHeight = 0.5f,phaseDelay = 0.5f,dropDelay = 3f;
    public PuzzlePusher samplePusher;
    public PuzzleTile samplePuzzle;
    public PuzzlePusherAdder samplePusherAdder;
    public GameObject touchPlaneObject;
    public Collider touchPlane;

    public CommonTouchContral touchContral;
    public Phase phase;

    public enum Dir { left, right };
    public enum Phase { idle, push, drop, insert, pushDelay, dropDelay, insertDelay };


    List<List<PuzzleTile>> puzzleMap;
    public PuzzleInserter inserter;
    List<PuzzlePusher>[] pusherBars;
    List<PuzzlePusher> pusherQuery;
    List<PuzzleTile> dropPuzzleCache;
    PuzzlePusherAdder[] puzzlePusherAdders;
    CommonRandomTable randomTable;
    int insertQuery;

    int[] cacheHeight;
    float phaseWait,dropWait;

    bool puzzleDirty = false, pusherDirty = false;
    //bool phasePush = false, phaseDrop = false ,phaseInsert = false; 
    Vector3 puzzleOffset,pusherOffsetLeft,pusherOffsetRight;

    //interfaces

    public void DropPuzzle(int col,PuzzleTile p) {
        p.SetContainer(this);
        dropPuzzleCache.Add(p);
        p.transform.position = new Vector3(col, cacheHeight[col] + height + 3f, 0f) + puzzleOffset;
        cacheHeight[col]++; 
    }

    public void RemovePuzzle(int col, int row) {
        List<PuzzleTile> l = puzzleMap[col];
        if (l.Count > row && l[row]!=null)
        {
            PuzzleTile p = l[row];
            
            l[row] = null;
            p.Kill();

            puzzleDirty = true;
        }
        
    }

    public bool RemovePuzzle(int col, int row, PuzzleTile.PType t)
    {
        List<PuzzleTile> l = puzzleMap[col];
        if (l.Count > row && l[row] != null)
        {
            PuzzleTile p = l[row];
            if (p.puzzleType == t)
            {
                l[row] = null;
                p.Kill();
                puzzleDirty = true;
                return true;
            }            
        }
        return false;
        
    }

    public void InsertPuzzle() {
        float yOffset = -1f;
        while (insertQuery > 0)
        {
            for (int i = 0; i < width; i++) {
                PuzzleTile p = Instantiate(samplePuzzle, puzzleOffset + new Vector3(i,yOffset,0f),Quaternion.identity, transform);
                p.SetPuzzleType((PuzzleTile.PType)randomTable.Get());
                p.SetContainer(this);
                puzzleMap[i].Insert(0, p);
            }
            yOffset -= 1f; 
            insertQuery--;
        }
    }

    public void InsertPuzzleRow() {
        insertQuery++;
    }
        
    public PuzzleTile GetPuzzleTile(int col,int row)
    {

        if (puzzleMap.Count > col && puzzleMap[col].Count > row)
        {
            return puzzleMap[col][row];    
        }
        return null;
    }

    public void AddPusherToQuery(PuzzlePusher p) {
        if (!pusherQuery.Contains(p))
        {
            if(SwapPusher(p))
                pusherQuery.Add(p);
        }
    }

    public void PushPuzzleRow(Dir dir,int row,PuzzleTile.PType t)
    {
        PuzzleTile p;
        int max=0, min=0, step=1;
        switch (dir) {
            case Dir.left:
                min = 0;
                max = puzzleMap.Count;
                step = 1;
                break;
            case Dir.right:
                max = -1;
                min = puzzleMap.Count-1;
                step = -1;
                break;
        }

        for (int i = max - step; i != min - step; i -= step)
        {
            p = GetPuzzleTile(i, row);
            if (p != null)
            {
                if (p.puzzleType == t)
                {
                    RemovePuzzle(i, row);
                }
            }
        }
        for(int i = min; i != max; i+=step)
        {
            Debug.Log("min=" + min + ";i=" + i);
            p = GetPuzzleTile(i, row);
            if (p != null)
            {
                puzzleMap[i][row] = null;
                if (puzzleMap[min].Count > row)
                {
                    puzzleMap[min][row] = p;
                }
                else
                {
                    puzzleMap[min].Add(p);
                }

                Vector3 pos = new Vector3(min, row, 0f) + puzzleOffset;
                p.Move(pos);
                min += step;
            }
        }

    }

    public void AddPusher(Dir dir)
    {
        PuzzlePusher p = Instantiate(samplePusher, pusherOffsetLeft, Quaternion.identity, transform);
        p.SetPuzzleType((PuzzleTile.PType)randomTable.Get());
        p.SetDirection(dir);
        p.SetContainer(this);
        switch (dir) {
            case Dir.left:
                pusherBars[1].Add(p);
                p.dir = Dir.left;
                break;
            case Dir.right:
                pusherBars[0].Add(p);
                p.dir = Dir.right;
                break;
        }
        pusherDirty = true;
    }

    public void RemovePusher(Dir dir, int row)
    {
        int bar = 0;
        switch (dir)
        {
            case Dir.left:
                bar = 1;
                break;
            case Dir.right:
                bar = 0;
                break;
        }

        if (pusherBars[bar].Count > row)
        {
            PuzzlePusher p = pusherBars[bar][row];
            pusherBars[bar][row] = null;
            if (p != null)
            {
                p.Kill();
            }
            pusherDirty = true;
        }

    }

    public void PuzzleEndMove() {
        puzzleDirty = true;
    }

    public void TouchChange(CommonTouchContral.TouchCursor t) {
        bool hited = false;
        switch (t.phase)
        {
            case CommonTouchContral.TouchCursor.Phase.Down:
                RaycastHit hit;
                /*
                foreach (PuzzlePusherAdder pa in puzzlePusherAdders)
                {
                    RaycastHit hit;
                    if (pa.collider.Raycast(t.ray, out hit, Mathf.Infinity))
                    {
                        pa.OnClick(t);
                        t.Use();
                        hited = true;
                        break;
                    }
                }
                if (hited) break;
                */

                if(inserter.collider.Raycast(t.ray, out hit, Mathf.Infinity))
                {
                    inserter.OnTouchDown(t);
                    t.Use();
                    hited = true;
                }

                foreach (PuzzlePusher p in pusherBars[0])
                {
                    
                    if (p.collider.Raycast(t.ray, out hit, Mathf.Infinity))
                    {
                        p.OnTouchDown(t);
                        t.Use();
                        hited = true;
                        break;
                    }
                }
                if (hited) break;

                foreach (PuzzlePusher p in pusherBars[1])
                {
                    if (p.collider.Raycast(t.ray, out hit, Mathf.Infinity))
                    {
                        p.OnTouchDown(t);
                        t.Use();
                        break;
                    }
                }
                break;

            case CommonTouchContral.TouchCursor.Phase.Up:
                

                break;
        }
    }


    //private method

    void Awake()
    {
        puzzleOffset = new Vector3(-width/2f+0.5f,-height/2f,0f);

        pusherOffsetLeft = new Vector3(-width / 2f - 1f, -height / 2f, 0f);
        pusherOffsetRight = new Vector3(+width / 2f + 1f, -height / 2f, 0f);

        touchContral.AddListener(this);

        puzzleMap = new List<List<PuzzleTile>>();
        for (int i=0; i < width; i++) { 
            List<PuzzleTile> l = new List<PuzzleTile>();
            l.Capacity = height + 1;
            puzzleMap.Add(l);
        }

        pusherBars = new List<PuzzlePusher>[2];
        pusherBars[0] = new List<PuzzlePusher>();
        pusherBars[0].Capacity = height + 1;
        pusherBars[1] = new List<PuzzlePusher>();
        pusherBars[1].Capacity = height + 1;

        dropPuzzleCache = new List<PuzzleTile>();
        cacheHeight = new int[width];
        for (int i = 0; i < width; i++) {
            cacheHeight[i] = 0;
        }
        pusherQuery = new List<PuzzlePusher>();

        puzzlePusherAdders = new PuzzlePusherAdder[2];

        touchPlane = touchPlaneObject.GetComponent<Collider>();

        randomTable = new CommonRandomTable(3, 4);

        puzzleDirty = false;
    }

    // Use this for initialization
    void Start () {
        PuzzlePusher p;
        PuzzlePusherAdder pa;


        /*
        pa = Instantiate(samplePusherAdder, pusherOffsetRight + new Vector3(0f, -1.5f, 0f), Quaternion.identity, transform);
        pa.SetContainer(this);
        pa.SetDirection(Dir.left);
        puzzlePusherAdders[1] = pa;

        pa = Instantiate(samplePusherAdder, pusherOffsetLeft + new Vector3(0f, -1.5f, 0f), Quaternion.identity, transform);
        pa.SetContainer(this);
        pa.SetDirection(Dir.right);
        puzzlePusherAdders[0] = pa;
        */

        randomTable = new CommonRandomTable(3, 4);
        for (int i = 0; i < height; i++) {
            AddPusher(Dir.left);
            AddPusher(Dir.right);
        }
        pusherDirty = true;

        phase = Phase.idle;
        phaseWait = phaseDelay;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate()
    {
        
        UpdatePusher();

        switch (phase)
        {
            case Phase.idle:
                if(!CheckGoToPushPhase())
                    if(!CheckGoToInsert())
                    CheckGoToDrop();                    
                break;
            case Phase.push:
                if (!CheckMoving()) {
                    phase = Phase.pushDelay;
                    phaseWait = phaseDelay;
                }
                break;
            case Phase.pushDelay:
                if (!CheckDelay()) GoToDropPhase();
                break;
            case Phase.insert:
                if (!CheckMoving())
                {
                    phase = Phase.insertDelay;
                    phaseWait = phaseDelay;
                }
                break;
            case Phase.insertDelay:
                if (!CheckDelay()) GoToDropPhase();
                break;
            case Phase.drop:
                if (!CheckMoving()) {
                    phase = Phase.dropDelay;
                    phaseWait = phaseDelay;
                }
                break;
            case Phase.dropDelay:
                if (!CheckDelay()) GoToIdle();
                break;

        }


    }

    bool CheckGoToDrop()
    {
        dropWait -= Time.fixedDeltaTime;
        if (dropWait <= 0) {
            GoToDropPhase();
            return true;
        }
        return false;
    }

    bool CheckGoToPushPhase()
    {
        if(pusherQuery.Count > 0)
        {
            PuzzlePusher p = pusherQuery[0];
            PushPuzzleRow(p.dir, p.row, p.puzzleType);
            pusherQuery.RemoveAt(0);
            RemovePusher(p.dir, p.row);
            AddPusher(p.dir);

            //MovePuzzle();
            phase = Phase.push;
            phaseWait = phaseDelay;
            return true;

        }

        return false;
    }

    bool CheckGoToInsert() {
        if (insertQuery > 0) {
            RemoveNullPusher();
            RemoveNullPuzzle();
            InsertPuzzle();
            MovePuzzle();
            phase = Phase.insert;
            return true;
        }
        return false;
    }

    void GoToDropPhase() {
        DropCache();
        RemoveNullPusher();
        RemoveNullPuzzle();
        MovePuzzle();
        phase = Phase.drop;
        phaseWait = phaseDelay;
    }

    void GoToIdle ()
    {
        phase = Phase.idle;
        phaseWait = phaseDelay;
        dropWait = dropDelay;
    }

    bool CheckDelay() {
        phaseWait -= Time.fixedDeltaTime;
        return phaseWait > 0f;
    }

    bool CheckMoving() {
        for (int c = 0; c < width; c++)
        {
            for (int r = 0; r < puzzleMap[c].Count; r++)
            {
                if (puzzleMap[c][r] != null && puzzleMap[c][r].moving) return true;
            }
        }
        return false;
    }

    void MovePuzzle() {
        for (int c = 0; c < width; c++)
        {
            for (int r = 0; r < puzzleMap[c].Count; r++)
            {
                if (puzzleMap[c][r] != null)
                {
                    Vector3 pos = new Vector3(c, r, 0f) + puzzleOffset;
                    puzzleMap[c][r].Move(pos);
                }
            }
        }
    }

    void RemoveNullPuzzle()
    {
        for (int c = 0; c < width; c++)
        {
            for (int r = 0; r < puzzleMap[c].Count; r++)
            {
                while (r < puzzleMap[c].Count&&puzzleMap[c][r] == null)
                {
                    puzzleMap[c].RemoveAt(r);
                }
            }
        }
        pusherDirty = true;
    }

    void RemoveNullPusher()
    {
        for (int c = 0; c < 2; c++)
        {
            for (int r = 0; r < pusherBars[c].Count; r++)
            {
                while (r<pusherBars[c].Count && pusherBars[c][r] == null)
                {
                    pusherBars[c].RemoveAt(r);
                }
            }
        }
    }

    void UpdatePusher() {
        if (pusherDirty) {
            for (int r = 0; r < pusherBars[0].Count; r++)
            {
                if (pusherBars[0][r] == null) continue;
                Vector3 pos = new Vector3(0f, r, 0f) + pusherOffsetLeft;
                pusherBars[0][r].Move(pos);
                pusherBars[0][r].row = r;
            }

            for (int r = 0; r < pusherBars[1].Count; r++)
            {
                if (pusherBars[1][r] == null) continue;
                Vector3 pos = new Vector3(0f, r, 0f) + pusherOffsetRight;
                pusherBars[1][r].Move(pos);
                pusherBars[1][r].row = r;
            }
            
            pusherDirty = false;
        }
    }


    void DropCache() {
        foreach (PuzzleTile p in dropPuzzleCache) {
            puzzleMap[p.col].Add(p);
        }
        dropPuzzleCache.Clear();
        for (int i = 0; i < width; i++)
            cacheHeight[i] = 0; 
    }

    bool SwapPusher(PuzzlePusher p) {
        List<PuzzlePusher> bar = pusherBars[1];
        switch (p.dir)
        {
            case Dir.left:
                bar = pusherBars[1];
                break;
            case Dir.right:
                bar = pusherBars[0];
                break;
        }

        int posrow = Mathf.RoundToInt(p.transform.position.y - pusherOffsetLeft.y);
        if (posrow >= bar.Count) posrow = bar.Count - 1;
        if (posrow < 0) posrow = 0;
        if (posrow != p.row) {
            bar[p.row] = bar[posrow];
            bar[posrow] = p;
        }
        pusherDirty = true;
        return true;
    }

}
