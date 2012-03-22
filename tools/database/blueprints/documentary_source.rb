# encoding: utf-8
DocumentarySource.destroy_all

DocumentarySource.blueprint do
  case_id { Case.all.sample.id }

  if rand(2) > 0
    reported_person_id { Person.all.sample.id }
    reported_institution_id { Institution.all.sample.id }
    reported_affiliation_type_id { AffiliationType.all.sample.id }
  else
    reported_institution_id { Institution.all.sample.id }
  end

  name { Faker::Lorem.paragraph(rand(6)) }
  additional_info { Faker::Lorem.paragraph(rand(6)) }
  date { Date.today }
  access_date { Date.today }
  documentary_source_type_id { DocumentarySourceType.all.sample.id }
  site_name { Faker::Lorem.paragraph(rand(6)) }
  url { ("http://" + Faker::Internet.domain_name) }
  access_date { Date.today }
  language_id { Language.all.sample.id }
  indigenous_language_id { IndigenousLanguage.all.sample.id }
  reliability_level_id { ReliabilityLevel.all.sample.id }
  observations { Faker::Lorem.paragraph(rand(6)) }
  comments { Faker::Lorem.paragraph(rand(6)) }

end
