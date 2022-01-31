#pragma once

#ifndef VIRTUAL_ACTUATOR_H
#define VIRTUAL_ACTUATOR_H

namespace AURS
{
    namespace Actuators
    {
        class VirtualActuator : public VirtualModule
        {
        public:
            VirtualActuator() : VirtualModule("") {}
            VirtualActuator(String moduleID) : VirtualModule(moduleID) {}
        }; // class VirtualActuator
    }      // namespace Actuators
    using namespace Actuators;
} // namespace AURS

using VirtualActuator = AURS::Actuators::VirtualActuator;

#include "VirtualMotor.h"
#include "VirtualSerial.h"

#endif