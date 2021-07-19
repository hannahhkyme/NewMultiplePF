using System;
using System.Collections.Generic;
namespace MultiplePF
{
    class MainClass
    {
        
        public List<double> real_range_list;
        public MainClass()
        {
            MyGlobals.robot_list = new List<Robot>();
            this.real_range_list = new List<double>();
        }

        public void update_real_range_list()
        {
            this.real_range_list = new List<double>();
            for (int i = 0; i < MyGlobals.robot_list.Count; i++)
            {
                double real_range1 = MyGlobals.robot_list[i].calc_range_error(MyGlobals.shark_list[0]);
                this.real_range_list.Add(real_range1);
            }
        }
        public void update_robot_list()
        {
            for (int i = 0; i < MyGlobals.robot_list.Count; i++)
            {
                MyGlobals.robot_list[i].update_robot_position();
            }
        }

        public double calc_range_error(List<double> predict_shark_location)
        {
            double x_component = Math.Pow((MyGlobals.s1.X - predict_shark_location[0]), 2);
            double y_component = Math.Pow((MyGlobals.s1.Y - predict_shark_location[1]), 2);
            double range_error = Math.Sqrt(x_component + y_component);
            return range_error;
        }
        public static void Main(string[] args)
        {
            // create Main Class
            MainClass main = new MainClass();
            // create Sharks
            //create a list of shark objs
            // [Shark1]
            MyGlobals.shark_list.Add(MyGlobals.s1);
            // create Robots

            Robot robot1 = new Robot();
            robot1.Y = 0;
            robot1.X = 45;
            Robot robot2 = new Robot();
            robot2.X = -100;
            robot2.Y = 45;
            MyGlobals.robot_list.Add(robot1);
            MyGlobals.robot_list.Add(robot2);
            // [Robot1, Robot2]
            // create PF
            // for all robots:
            ParticleFilter particle_filter = new ParticleFilter();
            particle_filter.create();
            // create particleList

            main.update_real_range_list();
            particle_filter.update_weights(main.real_range_list);

            Console.WriteLine("weight1");
            Console.WriteLine(particle_filter.particleList[0].W);
            Console.WriteLine("weight2");
            Console.WriteLine(particle_filter.particleList[1].W);

            Console.WriteLine("new particle1 x");
            Console.WriteLine(particle_filter.particleList[0].X);
            Console.WriteLine("new particle1 y");
            Console.WriteLine(particle_filter.particleList[0].Y);

            Console.WriteLine("new particle2 x");
            Console.WriteLine(particle_filter.particleList[1].X);
            Console.WriteLine("new particle2 y");
            Console.WriteLine(particle_filter.particleList[1].Y);
            double count = 0;
            while (true)
            {
                count += 1;
                //update Shark //update shark list
                MyGlobals.shark_list[0].update_shark();
                //update Robot // update robot list
                //main.update_robot_list();
                //update real_range_list
                main.update_real_range_list();

                //update PF
                particle_filter.update();
                particle_filter.update_weights(main.real_range_list);
                particle_filter.correct();
                Console.WriteLine("real shark x");
                Console.WriteLine(MyGlobals.shark_list[0].X);
                Console.WriteLine("real shark y");
                Console.WriteLine(MyGlobals.shark_list[0].Y);
                List<double> predict_shark_location = new List<double>();
                predict_shark_location = particle_filter.predicting_shark_location();
                Console.WriteLine("predicted shark x");
                Console.WriteLine(predict_shark_location[0]);
                Console.WriteLine("predicted shark y");
                Console.WriteLine(predict_shark_location[1]);
                Console.WriteLine("range error");
                Console.WriteLine(main.calc_range_error(predict_shark_location));
                /*
                Console.WriteLine("weight1");
                Console.WriteLine(particle_filter.particleList[0].W);
                Console.WriteLine("weight2");
                Console.WriteLine(particle_filter.particleList[1].W);
                Console.WriteLine("new particle1 x");
                Console.WriteLine(particle_filter.particleList[0].X);
                Console.WriteLine("new particle1 y");
                Console.WriteLine(particle_filter.particleList[0].Y);

                Console.WriteLine("new particle2 x");
                Console.WriteLine(particle_filter.particleList[1].X);
                Console.WriteLine("new particle2 y");
                Console.WriteLine(particle_filter.particleList[1].Y);
                */




            }
            
            // calculate new weight without the bearings

        }
    }
}
