using MOSU_LAB1.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSU_LAB1
{
    public class Tank
    {
        private double dt;
        private double xin1 = 0.0;  // початкове значення
        private double xin2 = 0.0;  // початкове значення
        private double xout1 = 0.0; // початкове значення
        private ComplexBlock block;
        private GainBlock in1Gain;
        private GainBlock in2Gain;
        private GainBlock in3Gain; // новий вхід xin1
        private GainBlock out1Gain;
        private GainBlock out2Gain;
        private LimitBlock limiter;

        public Tank(double dt)
        {
            this.dt = dt;

            in1Gain = new GainBlock(1.0);    // для xin1
            in2Gain = new GainBlock(0.5);    // для xin2
            in3Gain = new GainBlock(0.7);    // для xin3
            out1Gain = new GainBlock(-0.5);  // витік
            out2Gain = new GainBlock(-0.2);  // фіксований витік

            limiter = new LimitBlock(0, 10); // обмеження рівня
            block = new ComplexBlock();
            block.Add(new LimitedIntBlock(dt, 0, 10)); // інтегрування рівня
        }

        // Метод для обчислення рівня
        public double Step(double xin1, double xin2, double xout1)
        {
            this.xin1 = xin1;  // використання поточного значення
            this.xin2 = xin2;
            this.xout1 = xout1;

            double input1 = in1Gain.Calc(xin1);
            double input2 = in2Gain.Calc(xin2);
            double input3 = in3Gain.Calc(xin2);
            double output1 = out1Gain.Calc(xout1);
            double output2 = out2Gain.Calc(1);

            double dz = input1 + input2 + input3 + output1 + output2;
            double z = block.Calc(dz);
            return limiter.Calc(z);
        }

        public double X2 { get; set; } = 0.0; // початкове значення для xin2
        public double X3 { get; set; } = 0.0; // початкове значення для xout1
        public double X1 { get; set; } = 0.0; // початкове значення для xin1
    }
}