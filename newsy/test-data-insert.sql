INSERT INTO public."User" ("Id", "FirstName", "LastName", "UserName") VALUES 
('e95fa28b-1ed1-4a1b-a981-a4e608ca7cda', 'John', 'Doe', 'jd'),

('e95fa28b-1ed1-4a1b-a981-a4e608ca7cd9', 'Danny', 'Boy', 'db');


INSERT INTO public."Article" ("Id", "Content", "Created") VALUES 

('5ad823ec-e2fa-4a4a-aec8-914c7298661d', 
'This is simple article',
'2021-01-01'),

('5ad823ec-e2fa-4a4a-aec8-914c7298661e', 
'<html><head></head><body>This is HTML article</body></html>',
'2021-01-01'),

('5ad823ec-e2fa-4a4a-aec8-914c7298661f', 
'<html><head></head><body>This is another HTML article</body></html>',
'2021-01-01');


INSERT INTO public."ArticleUser" ("User_Id", "ArticleId") VALUES 

('e95fa28b-1ed1-4a1b-a981-a4e608ca7cd9', '5ad823ec-e2fa-4a4a-aec8-914c7298661d'),

('e95fa28b-1ed1-4a1b-a981-a4e608ca7cda', '5ad823ec-e2fa-4a4a-aec8-914c7298661e'),

('e95fa28b-1ed1-4a1b-a981-a4e608ca7cd9', '5ad823ec-e2fa-4a4a-aec8-914c7298661f'),
('e95fa28b-1ed1-4a1b-a981-a4e608ca7cda', '5ad823ec-e2fa-4a4a-aec8-914c7298661f');


/*

{
  "created": "2022-05-08T10:52:33.311Z",
  "content": "Another article2",
  "articleUser": [
    {
      "articleId": "00000000-0000-0000-0000-000000000000",
      "user_Id": "e95fa28b-1ed1-4a1b-a981-a4e608ca7cda"
    },
      {
        "articleId": "00000000-0000-0000-0000-000000000000",
        "user_Id": "e95fa28b-1ed1-4a1b-a981-a4e608ca7cd9"
      }
  ]
}

*/