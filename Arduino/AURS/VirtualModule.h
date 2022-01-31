#pragma once

#ifndef VIRTUAL_MODULE_H
#define VIRTUAL_MODULE_H

namespace AURS
{
    class VirtualModule
    {
    public:
        String moduleID;
        String value;
        VirtualModule(String moduleID)
        {
            this->moduleID = moduleID;
            value = "";
        }
        String GetModuleID()
        {
            return moduleID;
        }
    }; // class VirtualModule
} // namespace AURS

#include "Actuators/VirtualActuator.h"
#include "Sensors/VirtualSensor.h"

#endif