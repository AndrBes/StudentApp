namespace StudentWebApi.Services
{
    public class TransientService2
    {
        public int Counter { get; set; }

        public TransientService2(TransientService transientService)
        {
            transientService.Counter++;
        }
    }
}
