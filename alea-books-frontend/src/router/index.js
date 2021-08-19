import { createWebHistory, createRouter } from "vue-router";
import AuthPage from "../pages/AuthPage";
import BooksPage from "../pages/BooksPage";
import ComicBooksPage from "../pages/ComicBooksPage";
import DetailsPage from "../pages/DetailsPage";

const routes = [
	{
		name: "Auth",
		path: "/auth",
		component: AuthPage,
	},
	{
		name: "Books",
		path: "/books",
		component: BooksPage,
	},
	{
		name: "ComicBooks",
		path: "/comic-books",
		component: ComicBooksPage,
	},
	{
		name: "Details",
		path: "/details/:id",
		component: DetailsPage,
	},
];

const router = createRouter({
	history: createWebHistory(),
	routes: routes,
});

export default router;
