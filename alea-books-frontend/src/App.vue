<template>
	<LayoutComponent v-if="isLoggedIn">
		<router-view />
	</LayoutComponent>
	<router-view v-else />
</template>

<script>
	import LayoutComponent from "./components/Layout/LayoutComponent";
	import { computed, onMounted } from "vue";
	import { useStore } from "vuex";
	import jwtDecode from "jwt-decode";

	export default {
		name: "App",
		components: {
			LayoutComponent,
		},
		setup() {
			const store = useStore();

			const isLoggedIn = computed(() => store.state.isLoggedIn);

			onMounted(() => {
				const token = localStorage.getItem("token");
				if (token) {
					const decodedToken = jwtDecode(token);
					const tokenExpires = decodedToken.exp * 1000;
					const now = new Date();
					if (tokenExpires > now) {
						store.dispatch("setLoggedIn", {
							token: token,
						});
					}
				}
			});

			return {
				isLoggedIn,
			};
		},
	};
</script>

<style>
	* {
		margin: 0;
		padding: 0;
		box-sizing: border-box;
	}

	#app {
		font-family: Avenir, Helvetica, Arial, sans-serif;
		-webkit-font-smoothing: antialiased;
		-moz-osx-font-smoothing: grayscale;
		text-align: center;
		color: #2c3e50;
	}
</style>
