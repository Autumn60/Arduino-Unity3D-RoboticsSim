#pragma once

#ifndef VIRTUAL_SENSOR_H
#define VIRTUAL_SENSOR_H

namespace AURS
{
    namespace Sensors
    {
        class VirtualSensor : public VirtualModule
        {
        public:
            VirtualSensor() : VirtualModule("") {}
            VirtualSensor(String moduleID) : VirtualModule(moduleID) {}
        }; // class VirtualSensor
    }      // namespace Sensors;
    using namespace Sensors;
} // namespace AURS

using VirtualSensor = AURS::Sensors::VirtualSensor;

#include "VirtualEncoder.h"

#endif