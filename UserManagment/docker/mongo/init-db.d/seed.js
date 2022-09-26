db.users.drop();
db.users.insertOne({
    FullName: "full name",
    UserName: "admin",
    Email: "s",
    UserRole: 0,
    Password: "$2a$12$sAaKUqkrbK9.0Rza6E1MnexahwB1TSPW3kkJ6V1Gay.lp32kwpUr6"
})
db.users.insertOne({
    FullName: "string",
    UserName: "string",
    Email: "string",
    UserRole: 1,
    Password: "$2a$11$HlPAooBwBQn8b/j6ppjuLeXJgihMkb1Zg3DUdWA5/py8EfazeCLiq"
})