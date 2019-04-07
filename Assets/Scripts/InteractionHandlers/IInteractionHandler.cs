using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionHandler<T>{

    void Handle(T data, Action activateCrosshair, Action deactivateCrosshair);
}
