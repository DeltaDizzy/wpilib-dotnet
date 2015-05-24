﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib
{
    public class ADXL345_I2C : ADXL345
    {
        private const byte Address = 0x1D;

        private I2C i2c;

        public ADXL345_I2C(I2C.Port port, Interfaces.Range range)
        {
            i2c = new I2C(port, Address);
            i2c.Write(PowerCtlRegister, (int)PowerCtl.Measure);
            SetRange(range);
            HAL_Base.HAL.Report(HAL_Base.ResourceType.kResourceType_ADXL345, HAL_Base.Instances.kADXL345_I2C);
            //TODO: Add LiveWindow
        }

        protected override void WriteRange(byte value)
        {
            i2c.Write(DataFormatRegister, (byte)DataFormat.FullRes | value);
        }

        public override double GetAcceleration(Axes axis)
        {
            byte[] rawAccel = new byte[2];
            i2c.Read(DataRegister + (byte)axis, rawAccel.Length, rawAccel);
            return BitConverter.ToInt16(rawAccel, 0) * GsPerLSB;
        }

        public override AllAxes GetAccelerations()
        {
            AllAxes data = new AllAxes();
            byte[] rawData = new byte[6];
            i2c.Read(DataRegister, rawData.Length, rawData);
            data.XAxis = BitConverter.ToInt16(rawData, 0) * GsPerLSB;
            data.YAxis = BitConverter.ToInt16(rawData, 2) * GsPerLSB;
            data.ZAxis = BitConverter.ToInt16(rawData, 4) * GsPerLSB;
            return data;
        }

    }
}
