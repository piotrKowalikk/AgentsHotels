using System.Collections.Generic;

namespace HotelsLogic.Results
{
    public class Filters
    {
        public SearchedFilterResult AirConditioning { get; set; }
        public SearchedFilterResult FreeCancelation { get; set; }
        public SearchedFilterResult Wifi { get; set; }
        public SearchedFilterResult Bar { get; set; }
        public SearchedFilterResult Pool { get; set; }
        public SearchedFilterResult Fridge { get; set; }
        public SearchedFilterResult Microwave { get; set; }
        public SearchedFilterResult Safe { get; set; }
        public SearchedFilterResult Tv { get; set; }
        public SearchedFilterResult Massage { get; set; }
        public SearchedFilterResult Sauna { get; set; }
        public SearchedFilterResult Gym { get; set; }
        public SearchedFilterResult Spa { get; set; }
        public SearchedFilterResult Stars { get; set; }

        public List<SearchedFilterResult> ToList()
        {
            List<SearchedFilterResult> results = new List<SearchedFilterResult>(14);
            results.Add(this.AirConditioning);
            results.Add(this.FreeCancelation);
            results.Add(this.Wifi);
            results.Add(this.Bar);
            results.Add(this.Pool);
            results.Add(this.Fridge);
            results.Add(this.Microwave);
            results.Add(this.Safe);
            results.Add(this.Tv);
            results.Add(this.Massage);
            results.Add(this.Sauna);
            results.Add(this.Gym);
            results.Add(this.Spa);
            results.Add(this.Stars);

            return results;
        }
    }
}
