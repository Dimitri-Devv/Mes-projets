````
┌─────────────────────────┐
│         User            │
├─────────────────────────┤
│ - id : Long             │
│ - email : String        │
│ - passwordHash : String │
│ - pseudo : String       │
│ - avatarUrl : String    │
│ - role : UserRole       │
│ - createdAt : DateTime  │
├─────────────────────────┤
│ + register()            │
│ + login()               │
└─────────┬───────────────┘
          │1
          │
          │0..*
┌─────────────────────────┐
│         Review          │
├─────────────────────────┤
│ - id : Long             │
│ - title : String        │
│ - content : Text        │
│ - rating : int (1-10)   │
│ - createdAt : DateTime  │
├─────────────────────────┤
│ + publish()             │
│ + edit()                │
└─────────┬┬──────────────┘
          ││
   1      ││ 0..*
          ││
┌─────────▼┘               ┌─────────────────────────┐
│         Film            │1                        │
├─────────────────────────┤                         │
│ - id : Long             │                         │
│ - title : String        │                         │
│ - director : String     │                         │
│ - year : Int            │                         │
│ - genre : String        │                         │
│ - synopsis : Text       │                         │
│ - posterUrl : String    │                         │
│ - createdAt : DateTime  │                         │
├─────────────────────────┤                         │
│ + addFilm()             │                         │
└─────────────────────────┘                         │
                                                    │
                                                    │0..*
                                                    ▼
                                        ┌─────────────────────────┐
                                        │       Comment           │
                                        ├─────────────────────────┤
                                        │ - id : Long             │
                                        │ - content : Text        │
                                        │ - createdAt : DateTime  │
                                        ├─────────────────────────┤
                                        │ + write()               │
                                        └─────────┬───────────────┘
                                                  │1
                                                  │
                                                  ▼
                                             Review


┌─────────────────────────┐
│        LikeVote         │
├─────────────────────────┤
│ - id : Long             │
│ - value : int (1/-1)    │
├─────────────────────────┤
│ + toggleVote()          │
└───────┬─────────┬───────┘
        │1        │1
        ▼         ▼
      User      Review```
````
