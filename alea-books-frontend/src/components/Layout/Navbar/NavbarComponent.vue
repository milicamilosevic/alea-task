<template>
	<ul>
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
	</ul>
</template>

<script>
	import NavbarItem from "./NavbarItem";
	export default {
		components: {
			NavbarItem,
		},
	};
</script>

<style scoped>
	ul {
		display: flex;
		flex-direction: row;
		background-color: #90bc71;
		justify-content: center;
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

	.active-link {
		color: white;
		font-weight: bold;
	}
</style>
