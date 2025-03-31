import { defineConfig } from "umi";

export default defineConfig({
  initialState: {},
  model: {},
  request: {},
  proxy: {
    '/api': {
      'target': 'http://127.0.0.1:5254',
      'changeOrigin': true,
    }},
  routes: [
    { path: "/", component: "index" },
    { path: "/docs", component: "docs" },
    { path: "/user", component: "user" },
    { path: "/auth", component: "auth" },
    {
      path: "/user/:id",
      component: "user/$id"
    }
  ],
  npmClient: 'npm',
});
