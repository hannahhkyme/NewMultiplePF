﻿using System;
using System.Collections.Generic;
using System.Linq;
namespace MultiplePF
{
    internal class ParticleFilter
    {
        public int NUMBER_OF_PARTICLES;
        public double Current_Time;
        public List<Particle> particleList = new List<Particle>();
        public Robot r1;
        public List<double> w1_list_x;
        public List<double> w1_list_y;

        public List<double> w2_list_x;
        public List<double> w2_list_y;

        public List<double> w3_list_x;
        public List<double> w3_list_y;

        public List<double> errorList;

        public ParticleFilter()
        {
            this.Current_Time = 0;
            this.NUMBER_OF_PARTICLES = 2;
            this.r1 = new Robot();
            this.w1_list_x = new List<double>();
            this.w2_list_x = new List<double>();
            this.w3_list_x = new List<double>();
            this.w1_list_y = new List<double>();
            this.w2_list_y = new List<double>();
            this.w3_list_y = new List<double>();
            this.errorList = new List<double>();
        }

   
    
    public void create()
    {
           /* 
            for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
            {
                particleList.Add(new Particle());
            }
            */
            
            Particle particle1 = new Particle();
            particle1.X = 45;
            particle1.Y = 45;
            particleList.Add(particle1);
            
            Particle particle2 = new Particle();
            particle2.X = 0;
            particle2.Y = 0;
            
            particleList.Add(particle2);
            /*
            Particle particle3 = new Particle();
            particle3.Y = -100;
            particle3.X = -100;
            particleList.Add(particle3);
            */
            
        }
    public void update()
    {
        // updates particles while simulated
        // returns new list of updated particles

        for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
        {
            particleList[i].updateParticles();
        }

    }
    public void update_weights(List<double> real_range_list)// real_range_list
    {
            // normalize new weights for each new shark measurement

            List<double> particle_range_list = new List<double>();
            List<double> particle_range_list2 = new List<double>();
            for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
            {
                double particle_range = particleList[i].calc_particle_range(MyGlobals.robot_list[0]);
                double particle_range2 = particleList[i].calc_particle_range(MyGlobals.robot_list[1]);
                particle_range_list.Add(particle_range);
                particle_range_list2.Add(particle_range2);
                particleList[i].weight(real_range_list[0], particle_range_list[i], real_range_list[1], particle_range_list2[i]);// real_range_list
                // real_range_list = [[range1], [range1]]
                // weight:
                //          
            }
            // for robots

        }
        public List<double> normalize(List<List<double>> weight_list)
    {
        // for each list of weights in weight_list, normalize them individually
        List<List<double>> individual_normalized_list = new List<List<double>>();
        for (int i = 0; i < weight_list.Count; ++i)
        {
            double denominator = weight_list[i].Max();
            List<double> current_weight_list = weight_list[i];
            List<double> first_normalized_list = new List<double>();
            for (int j = 0; j < weight_list[i].Count; ++j)
            {
                current_weight_list[j] = (1 / denominator) * current_weight_list[j];

                first_normalized_list.Add(current_weight_list[j]);
            }
            individual_normalized_list.Add(first_normalized_list);

        }
        // adds the normalized weights of weight_list to the final list
        List<double> added_list = new List<double>();
        for(int k =0; k< individual_normalized_list[0].Count; ++k)
        {
                double new_weight = 0;
                for (int j = 0; j< individual_normalized_list.Count; ++j)
                {
                    new_weight += individual_normalized_list[j][k];
                }
                added_list.Add(new_weight);
        }
        // normalize the added list now
        double final_denominator = added_list.Max();
        List<double> normalized_list = new List<double>();
        for (int j = 0; j < added_list.Count; ++j)
        {
            added_list[j] = (1 / final_denominator) * added_list[j];

            normalized_list.Add(added_list[j]);
        }
        return normalized_list;
    }
    public void correct()
    {
            //corrects the particles, adding more copies of particles based on how high the weight is
            List<Particle> tempList = new List<Particle>();

        for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
        {
            if (particleList[i].W <= 0.333)
            {
                Particle particle1 = particleList[i].DeepCopy();
                tempList.Add(particle1);


            }
            else if (particleList[i].W <= 0.666)
            {
                Particle particle1 = particleList[i].DeepCopy();
                tempList.Add(particle1);
                Particle particle2 = particleList[i].DeepCopy();
                tempList.Add(particle2);

            }
            else
            {
                Particle particle1 = particleList[i].DeepCopy();
                tempList.Add(particle1);
                Particle particle2 = particleList[i].DeepCopy();
                tempList.Add(particle2);
                Particle particle3 = particleList[i].DeepCopy();
                tempList.Add(particle3);
                Particle particle4 = particleList[i].DeepCopy();
                tempList.Add(particle4);
            }

        }
            particleList = new List<Particle>();
            for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
            {
                int index = MyGlobals.random_num.Next(0,tempList.Count);
                Particle particleIndex = tempList[index].DeepCopy();
                particleList.Add(particleIndex);

            }
    }
    
    public void weight_list_x()
    {
       w1_list_x = new List<double>();
       w2_list_x = new List<double>();
       w3_list_x = new List<double>();
        for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
        {
            if (particleList[i].W <= 0.333)
            {
                w1_list_x.Add(particleList[i].X);
            }
            else if (particleList[i].W <= 0.666)
            {
                w2_list_x.Add(particleList[i].X);
            }
            else
            {
                w3_list_x.Add(particleList[i].X);
            }

        }

    }
        public void weight_list_y()
        {
            w1_list_y = new List<double>();
            w2_list_y = new List<double>();
            w3_list_y = new List<double>();
            for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
            {
                if (particleList[i].W <= 0.333)
                {
                    w1_list_y.Add(particleList[i].Y);
                }
                else if (particleList[i].W <= 0.666)
                {
                    w2_list_y.Add(particleList[i].Y);
                }
                else
                {
                    w3_list_y.Add(particleList[i].Y);
                }
            }
        }
    public List<double> predicting_shark_location()
            {
            double particle_total_x = 0;
            double particle_total_y = 0;
            for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
                {
                particle_total_x += particleList[i].X;
                particle_total_y += particleList[i].Y;
            }
            double particle_mean_x = particle_total_x / NUMBER_OF_PARTICLES;
            double particle_mean_y = particle_total_y / NUMBER_OF_PARTICLES;
            List<double> mean_particle = new List<double>();
            mean_particle.Add(particle_mean_x);
            mean_particle.Add(particle_mean_y);
            return mean_particle;
            }
    }
    

}

