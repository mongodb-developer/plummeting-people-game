const { MongoClient } = require("mongodb");
const Express = require("express");
const BodyParser = require('body-parser');

const server = Express();
const client = new MongoClient(process.env["ATLAS_URI"]);

server.use(BodyParser.json());
server.use(BodyParser.urlencoded({ extended: true }));

var collection;

server.post("/plummies", async (request, response, next) => {
    try {
        let result = await collection.insertOne(request.body);
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.get("/plummies", async (request, response, next) => {
    try {
      let result = await collection.find({}).toArray();
      response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.get("/plummies/:plummie_tag", async (request, response, next) => {
    try {
        let result = await collection.findOne({ "plummie_tag": request.params.plummie_tag });
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.put("/plummies/:plummie_tag", async (request, response, next) => {
    try {
        let result = await collection.updateOne(
            { "plummie_tag": request.params.plummie_tag },
            { "$set": request.body }
        );
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.delete("/plummies/:plummie_tag", async (request, response, next) => {
    try {
        let result = await collection.deleteOne({ "plummie_tag": request.params.plummie_tag });
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.listen("3000", async () => {
    try {
        await client.connect();
        collection = client.db("plummeting-people").collection("plummies");
        console.log("Listening at :3000...");
    } catch (e) {
        console.error(e);
    }
});