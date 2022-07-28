import { Database } from "@nozbe/watermelondb";
import LokiJSAdapter from "@nozbe/watermelondb/adapters/lokijs";

import { mySchema } from "models/schema";
import Blog from "models/Blog";
import Post from "models/Post";
import Comment from "models/Comment";

const adapter = new LokiJSAdapter({
  dbName: "WatermelonDemo",
  schema: mySchema,
});

export const database = new Database({
  adapter,
  modelClasses: [Blog, Post, Comment],
  actionsEnabled: true,
});
