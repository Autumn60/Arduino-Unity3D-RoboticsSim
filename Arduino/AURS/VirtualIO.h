#pragma once
#ifndef VIRTUAL_IO_H
#define VIRTUAL_IO_H

#ifndef AURS_MAX_PIN_COUNT
#define AURS_MAX_PIN_COUNT 10
#endif

namespace AURS
{
    namespace IO
    {
        class IOManager
        {
            IOManager() {}
            IOManager(const IOManager &) = delete;
            IOManager &operator=(const IOManager &) = delete;

            uint8_t writeCounter;
            uint8_t readCounter;

            VirtualActuator va[AURS_MAX_PIN_COUNT];
            VirtualSensor vs[AURS_MAX_PIN_COUNT];

            int FindActuatorID(String moduleID)
            {
                moduleID = "P" + moduleID;
                for (int i = 0; i < writeCounter; i++)
                {
                    if (moduleID == va[i].moduleID)
                        return i;
                }
                return -1;
            }

            int FindSensorID(String moduleID)
            {
                moduleID = "P" + moduleID;
                for (int i = 0; i < readCounter; i++)
                {
                    if (moduleID == vs[i].moduleID)
                        return i;
                }
                return -1;
            }

        public:
            ~IOManager()
            {
                writeCounter = 0;
                readCounter = 0;
            }

            static IOManager &get()
            {
                static IOManager iom;
                return iom;
            }

            void pinMode(String pinName, int mode) // mode: INPUT->0, OUTPUT->1, INPUT_PULLUP->2
            {
                switch (mode)
                {
                case 0:
                case 2:
                    if (readCounter >= AURS_MAX_PIN_COUNT)
                        return;
                    vs[readCounter].moduleID = "P"+pinName;
                    vs[readCounter].value = "0";
                    UnityConnection.Add(&vs[readCounter]);
                    readCounter++;
                    break;
                case 1:
                    if (writeCounter >= AURS_MAX_PIN_COUNT)
                        return;
                    va[writeCounter].moduleID = "P"+pinName;
                    va[writeCounter].value = "0";
                    UnityConnection.Add(&va[writeCounter]);
                    writeCounter++;
                    break;
                }
            }

            void digitalWrite(String pinName, bool b)
            {
                int n = FindActuatorID(pinName);
                if (n != -1)
                    va[n].value = b ? "1" : "0";
            }

            void analogWrite(String pinName, uint8_t p)
            {
                int n = FindActuatorID(pinName);
                if (n != -1)
                {
                    va[n].value = String(p);
                }
                else
                {
                    this->pinMode(pinName, OUTPUT);
                    va[writeCounter - 1].value = String(p);
                }
            }

            bool digitalRead(String pinName)
            {
                int n = FindSensorID(pinName);
                if (n != -1)
                    return vs[n].value;
                return 0;
            }

            uint8_t analogRead(String pinName)
            {
                int n = FindActuatorID(pinName);
                if (n != -1)
                {
                    return constrain(va[n].value.toInt(), 0, 255);
                }
                else
                {
                    pinMode(pinName, INPUT);
                    return 0;
                }
            }
        };
    }
} // namespace AURS

#define VirtualIO AURS::IO::IOManager::get()

#define virtualPinMode(pin, value) VirtualIO.pinMode(String(pin), value)
#define virtualDigitalWrite(pin, value) VirtualIO.digitalWrite(String(pin), value)
#define virtualAnalogWrite(pin, value) VirtualIO.analogWrite(String(pin), value)
#define virtualDigitalRead(pin) VirtualIO.digitalRead(String(pin))
#define virtualAnalogRead(pin) VirtualIO.analogRead(String(pin))

#endif