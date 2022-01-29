#pragma once

#ifndef AURS_H
#define AURS_H

#include "VirtualModule.h"

#ifndef AURS_MAX_ACTUATOR_COUNT
#define AURS_MAX_ACTUATOR_COUNT 10
#endif

#ifndef AURS_MAX_SENSOR_COUNT
#define AURS_MAX_SENSOR_COUNT 10
#endif

#ifndef AURS_SENDING_RATE
#define AURS_SENDING_RATE 100
#endif

#ifndef AURS_RECEIVING_RATE
#define AURS_RECEIVING_RATE 50
#endif

namespace AURS
{
    class Manager
    {
        Manager() {}
        Manager(const Manager &) = delete;
        Manager &operator=(const Manager &) = delete;

        VirtualActuator *virtualActuators[AURS_MAX_ACTUATOR_COUNT];
        VirtualSensor *virtualSensors[AURS_MAX_SENSOR_COUNT];

        uint8_t actuatorCount;
        uint8_t sensorCount;

        uint16_t sTimer, rTimer;

        int StringSplit(String data, char delimiter, String *dst)
        {
            int index = 0;
            int arraySize = (sizeof(data)) / sizeof((data[0]));
            int datalength = data.length();

            for (int i = 0; i < datalength; i++)
            {
                char tmp = data.charAt(i);
                if (tmp == delimiter)
                {
                    index++;
                    if (index > (arraySize - 1))
                        return -1;
                }
                else
                    dst[index] += tmp;
            }
            return (index + 1);
        }

        int FindModuleID(String moduleID){
            for(int i=0;i<sensorCount;i++){
                if(moduleID==virtualSensors[i]->moduleID)
                    return i;
            }
            return -1;
        }

    public:
        ~Manager()
        {
        }

        static Manager &get()
        {
            static Manager m;
            return m;
        }

        void Init()
        {
            actuatorCount = 0;
            sensorCount = 0;
            for (int i = 0; i < AURS_MAX_ACTUATOR_COUNT; i++)
                virtualActuators[i] = nullptr;
            for (int i = 0; i < AURS_MAX_SENSOR_COUNT; i++)
                virtualSensors[i] = nullptr;
        }
        void Set()
        {
            sTimer = rTimer = 0;
        }
        void Update()
        {
            const uint16_t currentTime = millis();
            if (currentTime - sTimer > AURS_SENDING_RATE)
            {
                sTimer = currentTime;
                Send();
            }
            if (currentTime - rTimer > AURS_RECEIVING_RATE)
            {
                rTimer = currentTime;
                Receive();
            }
        }
        void Send()
        {
            String str = "";
            for (int i = 0; i < actuatorCount; i++)
            {
                str += (virtualActuators[i]->moduleID + ":" + virtualActuators[i]->value + ",");
            }
            Serial.println(str);
        }
        void Receive()
        {
            if (Serial.available() < 1)
                return;
            String cmd = Serial.readStringUntil('\n');
            String cmds[AURS_MAX_SENSOR_COUNT] = {"\n"};
            String pair[AURS_MAX_SENSOR_COUNT][2] = {"\n"};

            int index = StringSplit(cmd, ',', cmds);

            for (int i = 0; i < index; i++)
            {
                int index_ = StringSplit(cmds[i], ':', pair[i]);
                if (index_ == 2)
                {
                    if (i == 0)
                        pair[i][0].replace("\n", "");
                    const int key = FindModuleID(pair[i][0]);
                    if(key!=-1)
                        virtualSensors[key]->value = pair[i][1];
                }
            }
        }

        void Add(VirtualActuator *VA)
        {
            if (actuatorCount >= AURS_MAX_ACTUATOR_COUNT)
                return;
            virtualActuators[actuatorCount] = VA;
            actuatorCount++;
        }
        void Add(VirtualSensor *VS)
        {
            if (actuatorCount >= AURS_MAX_SENSOR_COUNT)
                return;
            virtualSensors[sensorCount] = VS;
            sensorCount++;
        }
    }; // class Manager
} // namespace AURS

#define UnityConnection AURS::Manager::get()

#endif