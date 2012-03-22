# encoding: utf-8
InformationSource.destroy_all

InformationSource.blueprint do
  case_id { Case.all.sample.id }

  if rand(2) > 0
    source_person_id { Person.all.sample.id }
    source_institution_id { Institution.all.sample.id }
    source_affiliation_type_id { AffiliationType.all.sample.id }
  else
    source_institution_id { Institution.all.sample.id }
  end

  if rand(2) > 0
    reported_person_id { Person.all.sample.id }
    reported_institution_id { Institution.all.sample.id }
    reported_affiliation_type_id { AffiliationType.all.sample.id }
  else
    reported_institution_id { Institution.all.sample.id }
  end

  source_information_type_id { SourceInformationType.all.sample.id }

  date_type_id { DateType.all.sample.id }
  date { Date.today }
  language_id { Language.all.sample.id }
  indigenous_language_id { IndigenousLanguage.all.sample.id }
  reliability_level_id { ReliabilityLevel.all.sample.id }
  observations { Faker::Lorem.paragraph(rand(6)) }
  comments { Faker::Lorem.paragraph(rand(6)) }

end