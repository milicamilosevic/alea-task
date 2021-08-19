<template>
	<ul>
		<li class="search-container">
			<input type="text" placeholder="Pretražite..." />
			<button>Pretraži</button>
		</li>
		<li>
			<router-link to="/books" active-class="active-link">
				<NavbarItem name="Knjige" />
			</router-link>
		</li>
		<li>
			<router-link to="/comic-books" active-class="active-link">
				<NavbarItem name="Stripovi" />
			</router-link>
		</li>
		<li class="logout" @click="logoutHandler">
			Odjava
		</li>
	</ul>
</template>

<script>
	import NavbarItem from "./NavbarItem";
	import { useStore } from "vuex";
	import { useRouter } from "vue-router";

	export default {
		components: {
			NavbarItem,
		},
		setup() {
			const store = useStore();
			const router = useRouter();

			const logoutHandler = () => {
				store.dispatch("setLoggedOut");
				router.push("/auth");
			};

			return {
				logoutHandler,
			};
		},
	};
</script>

<style scoped>
	ul {
		display: flex;
		flex-direction: row;
		background-color: #90bc71;
		justify-content: center;
		position: relative;
		height: 35px;
		align-items: center;
	}

	li {
		cursor: pointer;
		list-style-type: none;
		padding: 5px 20px;
	}

	a {
		color: rgb(231, 231, 231);
		text-decoration: none;
		font-size: 16px;
	}

	a:hover {
		text-decoration: underline;
	}

	.active-link {
		color: white;
		font-weight: bold;
	}

	.search-container {
		position: absolute;
		left: 0;
	}

	.search-container input {
		font-size: 16px;
		margin-right: 10px;
		outline: none;
		padding: 1px 5px;
		border-radius: 3px;
		border: none;
	}
	.search-container button {
		font-size: 14px;
		padding: 2px 15px;
		background-color: transparent;
		border: 2px white solid;
		color: white;
		border-radius: 30px;
		cursor: pointer;
	}

	.search-container button:hover {
		background-color: rgba(0, 0, 0, 0.1);
	}
	.search-container button:active {
		box-shadow: 0 0 5px 3px rgba(0, 0, 0, 0.3);
	}

	.logout {
		position: absolute;
		right: 0;
		color: white;
	}
</style>
