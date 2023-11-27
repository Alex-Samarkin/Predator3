// Predator3
// PLibrary1
// Virus.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 2:39 09 09 2023

namespace PLibrary1;

public class Virus 
{
    /// <summary>
    /// название вируса
    /// </summary>
    public string Name { get; set; } = "SARS-COVID";
    /// <summary>
    /// описание
    /// </summary>
    public string Description { get; set; } = "";
    /// <summary>
    /// время инкубационного периода (среднее)
    /// </summary>
    public double ExposedDuration { get; set; } = 10;
    /// <summary>
    /// время течения заболевания после инкубационного периода
    /// </summary>
    public double InfectedDuration { get; set; } = 16;
    /// <summary>
    /// время иммунитета после заболевания или вакцинации
    /// </summary>
    public double ImmunityDuration { get; set; } = 120;
    /// <summary>
    /// вероятность заболевания приконтакте с носителем
    /// </summary>
    public double Kinf { get; set; } = 0.8;
    /// <summary>
    /// вероятность смерти после заболевания
    /// </summary>
    public double Kdead { get; set; } = 0.01;
    /// <summary>
    /// вероятность тяжелого течения заболевания
    /// </summary>
    public double Khard { get; set; } = 0.05;
}