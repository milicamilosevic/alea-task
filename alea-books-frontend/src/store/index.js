import { createStore } from "vuex";

export default createStore({
	state: {
		isLoggedIn: false,
	},
	mutations: {
		SET_LOGGED_IN: (state) => {
			state.isLoggedIn = true;
		},
		SET_LOGGED_OUT: (state) => {
			state.isLoggedIn = false;
		},
	},
	actions: {
		setLoggedIn: ({ commit }, payload) => {
			localStorage.setItem("token", payload.token);
			commit("SET_LOGGED_IN", payload);
		},
		setLoggedOut: ({ commit }, payload) => {
			localStorage.removeItem("token");
			commit("SET_LOGGED_OUT", payload);
		},
	},
});
