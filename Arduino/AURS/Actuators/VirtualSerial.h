#pragma once

#ifndef VIRTUAL_SERIAL_H
#define VIRTUAL_SERIAL_H

namespace AURS
{
    namespace Actuators
    {
        class VirtualSerial : public VirtualActuator
        {
        public:
            VirtualSerial() : VirtualActuator("VS")
            {
                value = "";
                str = "";
            }
            VirtualSerial(String moduleID) : VirtualActuator(moduleID)
            {
                value = "";
                str = "";
            }

            template <class T>
            void print(T arg)
            {
                value = "";
                String arg_str = String(arg);
                arg_str.replace(":", "->");
                arg_str.replace(" ", "_");
                arg_str.replace(",", " ");
                str += arg_str;
            }

            template <class T>
            void println(T arg)
            {
                print(arg);
                println();
            }

            void println()
            {
                value = str;
                str = "";
            }

        private:
            String str;
        }; // class VirtualMotor
    }      // namespace Actuators
} // namespace AURT

using VirtualSerial = AURS::Actuators::VirtualSerial;

#endif