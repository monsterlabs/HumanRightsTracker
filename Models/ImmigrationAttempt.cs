using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("immigration_attempts")]
    public class ImmigrationAttempt : ActiveRecordValidationBase<ImmigrationAttempt>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("cross_border_attempts_transit_country")]
        public int CrossBorderAttemptsTransitCountry { get; set; }

        [Property("cross_border_attempts_destination_country")]
        public int CrossBorderAttemptsDestinationCountry { get; set; }

        [Property("expulsions_from_destination_country")]
        public int ExpulsionsFromDestinationCountry { get; set; }

        [Property("expulsions_from_transit_country")]
        public int ExpulsionsFromTransitCountry { get; set; }

        [Property("time_spent_in_destination_country")]
        public string TimeSpentInDestinationCountry { get; set; }

        [Property("travel_companions")]
        public int TravelCompanions { get; set; }

        [BelongsTo("origin_country_id")]
        public Country OriginCountry { get; set; }

        [BelongsTo("origin_state_id")]
        public State OriginState { get; set; }

        [BelongsTo("origin_city_id")]
        public City OriginCity { get; set; }

        [BelongsTo("traveling_reason_id")]
        public TravelingReason TravelingReason { get; set; }

        [BelongsTo("destination_country_id")]
        public Country DestinationCountry { get; set; }

        [BelongsTo("transit_country_id")]
        public Country TransitCountry { get; set; }

        [BelongsTo("person_id")]
        public Person Person { get; set; }
    }
}
