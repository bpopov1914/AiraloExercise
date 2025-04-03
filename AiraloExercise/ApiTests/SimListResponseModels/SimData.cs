namespace AiraloExercise.ApiTests.SimListResponseModels
{
    public class SimData
    {
        public int id { get; set; }
        public string created_at { get; set; }
        public string iccid { get; set; }
        public Simable simable { get; set; }
    }
}
