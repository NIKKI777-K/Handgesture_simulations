# Flex Sensor Controller in Unity

This project uses an Arduino with flex sensors to control a 3D hand model's Unity, via serial communication. The Unity C# script reads sensor values and maps them to rotation angles for finger joints.

## Features

Reads analog values from an Arduino via serial port.
Maps flex sensor readings to realistic finger bending angles.
Applies real-time rotation to each joint (3 per finger) in Unity.
Handles calibration and clamping to avoid unrealistic movement.

### System Overview

Hardware: Arduino Uno/Nano + Flex Sensors
Software: Unity + C# + Serial Communication
