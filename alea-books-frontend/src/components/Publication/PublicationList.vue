<template>
	<div class="grid-container">
		<PublicationItem v-for="p in publications" :key="p.id" :publication="p" />
	</div>
</template>

<script>
	import { ref, onMounted } from "vue";
	import axios from "../../config/axios-config";
	import PublicationItem from "./PublicationItem";

	export default {
		props: {
			publicationType: String,
		},
		components: {
			PublicationItem,
		},
		setup(props) {
			const publications = ref([]);

			const fetchPublications = () => {
				axios
					.get(`publication/gettoprated?type=${props.publicationType}`)
					.then((response) => {
						console.log("Knjige", response.data);
						publications.value = response.data;
					});
			};

			onMounted(fetchPublications);

			return {
				publications,
			};
		},
	};
</script>

<style scoped>
	.grid-container {
		display: grid;
		grid-template-columns: 1fr 1fr 1fr 1fr 1fr;
		grid-template-rows: 1fr 1fr;
		row-gap: 20px;
		column-gap: 20px;
		margin: 20px 40px;
	}
</style>
