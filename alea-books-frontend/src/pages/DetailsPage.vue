<template>
	<PublicationDetails :publicationId="publicationId" />
</template>

<script>
	import { ref } from "vue";
	import { useRoute } from "vue-router";
	import PublicationDetails from "../components/Publication/PublicationDetails";

	export default {
		components: {
			PublicationDetails,
		},
		setup() {
			const route = useRoute();
			const publicationId = ref(route.params.id);

			return {
				publicationId,
			};
		},
	};
</script>

<style></style>
