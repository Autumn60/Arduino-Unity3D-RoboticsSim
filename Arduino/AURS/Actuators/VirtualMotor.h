#pragma once

#ifndef VIRTUAL_MOTOR_H
#define VIRTUAL_MOTOR_H

namespace AURS
{
    namespace Actuators
    {
        class VirtualMotor : public VirtualActuator
        {
        public:
            VirtualMotor() : VirtualActuator("")
            {
                value = "0";
            }
            VirtualMotor(String moduleID) : VirtualActuator(moduleID)
            {
                value = "0";
            }
            void attach(int pinNum)
            {
                if (moduleID == "")
                    moduleID = "M" + String(pinNum);
            }

            void writeMicroseconds(int pulse)
            {
                value = String((int)((constrain(pulse, 1000, 2000) - 1500) * 0.2));
            }
        }; // class VirtualMotor
    }      // namespace Actuators
} // namespace AURT

using VirtualMotor = AURS::Actuators::VirtualMotor;

#endif