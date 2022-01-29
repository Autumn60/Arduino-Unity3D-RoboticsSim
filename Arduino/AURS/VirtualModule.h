#pragma once

#ifndef VIRTUAL_MODULE_H
#define VIRTUAL_MODULE_H

namespace AURS
{
    class VirtualModule
    {
    protected:
        String moduleID;
        String value;

    public:
        VirtualModule(String moduleID)
        {
            this->moduleID = moduleID;
            value = "";
        }
        String GetModuleID()
        {
            return moduleID;
        }
        friend class Manager;
    }; // class VirtualModule
} // namespace AURS

#include "Actuators/VirtualActuator.h"
#include "Sensors/VirtualSensor.h"

#endif