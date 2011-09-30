# encoding: utf-8
InformationSource.destroy_all

InformationSource.blueprint do
  case_id { Case.all.sample.id }
  if rand(2) > 0
    information_sourceable_id { Person.all.sample.id }
    information_sourceable_type { "Person"}
  else
    information_sourceable_id { Institution.all.sample.id }
    information_sourceable_type { "Institution"}
  end

  if rand(2) > 0
    reported_personable_id { Person.all.sample.id }
    reported_personable_type { "Person"}
  else
    reported_personable_id { Institution.all.sample.id }
    reported_personable_type { "Institution"}
  end

  affiliation_type_id { AffiliationType.all.sample.id }
  date_type_id { DateType.all.sample.id }
  date { Date.today }
  language_id { Language.all.sample.id }
  indigenous_language_id { IndigenousLanguage.all.sample.id }
  reliability_level_id { ReliabilityLevel.all.sample.id }
  observations { Faker::Lorem.paragraph(rand(6)) }
  comments { Faker::Lorem.paragraph(rand(6)) }
  
end