SELECT p.Id, p.Title, p.URL, p.PublishDateTime, p.AuthorId, p.BlogId,
                                        a.Id ,a.FirstName, a.LastName,
                                        b.Id ,b.Title as BlogTitle ,b.URL as BlogURL
                                            FROM Post p 
                                            left join Author a on p.AuthorId = a.Id 
                                            Left Join Blog b on p.BlogId = b.Id 
                                            Where p.Id = 1