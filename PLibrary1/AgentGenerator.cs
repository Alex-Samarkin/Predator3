// Predator3
// PLibrary1
// AgentGenerator.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:46 10 09 2023

using MathNet;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;

using RandomDataGenerator;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace PLibrary1;

public class AgentGenerator
{
    public Agent NewAgent { get; set; } = new Agent();
    public int Seed { get; set; } = 42;
    public RandomSource Rnd { get; set; } = new MersenneTwister();

    private RandomizerFullName randomizer =
        new RandomizerFullName(new FieldOptionsFullName() { Female = true, Male = true });

    public double Xmax { get; set; } = 100;
    public double Ymax { get; set; } = 100;

    public double Vmax { get; set; } = 3;
    public double Amax { get; set; } = 2;
    public double dAmax { get; set; } = 0.5;

    public Agent BuildNewAgent()
    {
        Init();
        SetAgeGender();
        SetXYZ();
        SetVAAng();
        Koeff();
        return NewAgent;
    }


//01
    public void Init()
    {
        // ResetRng(Seed);
        NewAgent = new Agent();
        NewAgent.Name = randomizer.Generate()??"No Name";
    }

    //02
    public void SetAgeGender()
    {
        var age = Rnd.NextDouble()*100;
        if (age <13)
        {
            NewAgent.Age = Rnd.Next(1,15);
            NewAgent.Gender = Rnd.Next(100) > 51 ? GenderState.Female : GenderState.Male;
        }
        else
        {
            if (age < 27)
            {
                NewAgent.Age = Rnd.Next(15, 25);
                NewAgent.Gender = Rnd.Next(100)>55?GenderState.Female:GenderState.Male;
            }
            else
            {
                if (age < 62)
                {
                    NewAgent.Age = Rnd.Next(25, 55);
                    NewAgent.Gender = Rnd.Next(100) > 49 ? GenderState.Female : GenderState.Male;
                }
                else
                {
                    if (age < 84)
                    {
                        NewAgent.Age = Rnd.Next(55, 65);
                        NewAgent.Gender = Rnd.Next(100) > 43 ? GenderState.Female : GenderState.Male;
                    }
                    else
                    {
                        NewAgent.Age = Rnd.Next(65, 100);
                        NewAgent.Gender = Rnd.Next(100) > 29 ? GenderState.Female : GenderState.Male;
                    }
                }
            }
        }
    }

    //03
    public void SetXYZ()
    {
        NewAgent.x = Rnd.NextDouble() * Xmax;
        NewAgent.y = Rnd.NextDouble() * Ymax;
        NewAgent.z =  0;

    }

    //04
    public void SetVAAng()
    {
        NewAgent.v = Rnd.NextDouble() * Vmax;
        NewAgent.a = Rnd.NextDouble() * Amax;
        NewAgent.angle = ((Rnd.NextDouble()-0.5)*2.0)*Math.PI;
    }
    //05
    public void Koeff()
    {
        Koeff k = new Koeff();
        if (NewAgent.Age < 19)
        {
            k.Kdead = 0.1;
            k.Kinf = 0.2;
            k.Ki = Normal.Sample(Rnd,0,0.05);
            k.Ke = Normal.Sample(Rnd,0, 0.05);
            k.Khard = Rnd.NextDouble()/10.0;
            k.Kimm = Normal.Sample(Rnd, 0, 0.1);
        }
        if ((NewAgent.Age >= 19)&&(NewAgent.Age < 52))
        {
            k.Kdead = 0.8;
            k.Kinf = 0.8;
            k.Ki = Normal.Sample(Rnd, 0, 0.1);
            k.Ke = Normal.Sample(Rnd, 0, 0.1);
            k.Khard = Rnd.NextDouble();
            k.Kimm = Normal.Sample(Rnd, 0, 0.025);
        }
        if (NewAgent.Age >= 52)
        {
            k.Kdead = 1.1;
            k.Kinf = 1.2;
            k.Ki = Normal.Sample(Rnd, 0.5, 0.1);
            k.Ke = Normal.Sample(Rnd, 0, 0.1);
            k.Khard = Rnd.NextDouble()+0.5;
            k.Kimm = Normal.Sample(Rnd, 0, 0.1); 
        }
        NewAgent.Koeff = k;
    }

    public void AddVirus()
    {
        var vrs = new Virus();

        vrs.Kdead *= NewAgent.Koeff.Kdead;
        vrs.Kinf *= NewAgent.Koeff.Kinf;
        vrs.ExposedDuration = vrs.ExposedDuration * (1 + NewAgent.Koeff.Ke);
        vrs.InfectedDuration = vrs.InfectedDuration * (1 + NewAgent.Koeff.Ki);
        vrs.ImmunityDuration = vrs.ImmunityDuration * (1 + NewAgent.Koeff.Kimm);

        vrs.Khard = NewAgent.Koeff.Khard;
        
        NewAgent.Virus = vrs;
    }

    #region Util

    public void ResetRng(int NewSeed = 42)
    {
        Seed = NewSeed;
        Rnd = new MersenneTwister(Seed);
    }

    public int PopulationCount { get; set; } = 210000;
    public double MaleRate { get; set; } = 45.2;
    /*
    Age	    Всего	Age%		    М	    Ж	    M%	    Ж%
    0-14	89995	0.13	0.13	46252	43743	0.51	0.49
    15-24	90450	0.13	0.27	49956	40494	0.55	0.45
    25-54	234632	0.35	0.62	115690	118942	0.49	0.51
    55-64	152314	0.23	0.84	66069	86245	0.43	0.57
    65-100	105988	0.16	1.00	30661	75327	0.29	0.71
	        673379						
     */


    #endregion

}