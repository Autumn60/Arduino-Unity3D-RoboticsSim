#pragma once

#ifndef VIRTUAL_ENCODER_H
#define VIRTUAL_ENCODER_H

namespace AURS
{
    namespace Sensors
    {
        class VirtualEncoder : public VirtualSensor
        {
        public:
            VirtualEncoder(String moduleID) : VirtualSensor(moduleID) {}
            VirtualEncoder(int a, int b) : VirtualSensor("E" + String(a) + String(b)) {}
            long read()
            {
                return atol(value.c_str());
            }
        }; // class VirtualEncoder
    }      // namespace Sensors;
} // namespace AURS

using VirtualEncoder = AURS::Sensors::VirtualEncoder;

#endif