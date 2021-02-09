const mongoose = require("mongoose"),
  ObjectID = require("mongodb").ObjectID;
let db;

//Connect to MongoDB With Authentication.
exports.cnctDBAuth = (collectionname) => {
  const mongAuth = require("./mongoauth.json");
  mongoose.connect("mongodb://localhost:27017/" + collectionname, {
    auth: {
      authSource: "admin",
    },
    user: mongAuth.username,
    pass: mongAuth.pass,
    useNewUrlParser: true,
    useUnifiedTopology: true,
  });

  db = mongoose.connection;
  db.on("error", console.error.bind(console, "connection error:"));
  db.once("open", function () {
    console.log("Connected to MongoDB using collection " + collectionname);
  });
};

//Connect to MongoDB
exports.cnctDB = (collectionname) => {
  let dbLink = `mongodb://localhost/${collectionname}`;
  mongoose.connect(dbLink, { useNewUrlParser: true, useUnifiedTopology: true });

  db = mongoose.connection;
  db.on("error", console.error.bind(console, "connection error:"));
  db.once("open", function () {
    console.log("Connected to MongoDB using " + collectionname);
  });
};

exports.searchInDB = async (link) => {
  const regex = new RegExp(escapeRegex(link), "gi");
  let tmp = await db.collection("Links").find({ url: regex }).limit(500).toArray();
  return tmp;
};

function escapeRegex(text) {
  return text.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
}
