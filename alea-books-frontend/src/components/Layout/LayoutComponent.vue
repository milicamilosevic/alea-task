<template>
	<div>
		<NavbarComponent />
		<main>
			<slot />
		</main>
	</div>
</template>

<script>
	import NavbarComponent from "./Navbar/NavbarComponent";

	export default {
		components: {
			NavbarComponent,
		},
	};
</script>

<style scoped></style>
