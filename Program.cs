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
            Robot robot2 = new Robot();
            MyGlobals.robot_list.Add(robot1);
            MyGlobals.robot_list.Add(robot2);
            // [Robot1, Robot2]

            // create PF
            ParticleFilter particle_filter = new ParticleFilter();
            particle_filter.create(); 
            // create particleList
           
           
            //while True:
            //update Shark //update shark list
            MyGlobals.shark_list[0].update_shark();
            //update Robot // update robot list
            main.update_robot_list();
            //update real_range_list
            main.update_real_range_list();

            //update PF
            // weight(real_range_list)
            // calculate new weight without the bearings

        }
    }
}
