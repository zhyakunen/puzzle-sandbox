﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement {

    Vector3 Move();
    void SetSpeed(float speed);

}
