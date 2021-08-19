<template>
	<div class="details-wrapper" v-if="publication">
		<div class="image-container">
			<img :src="publication.image" :alt="publication.title" />
		</div>
		<div class="details-container">
			<div class="title">{{ publication.title }}</div>
			<div class="author-name">
				{{ `${publication.author.firstName} ${publication.author.lastName}` }}
			</div>
			<div class="description">
				<div>Sažetak:</div>
				<p>
					{{ publication.description }}
				</p>
			</div>
			<div class="release-date">
				<label>Datum izdavanja:</label>
				<div>
					{{ releaseDate }}
				</div>
			</div>
		</div>
		<div class="rating">
			<span>{{ publication.rating }}</span>
			<i class="fa fa-star" aria-hidden="true"></i>
		</div>
	</div>
	<div v-if="notFound">
		Nije pronađeno
	</div>
</template>

<script>
	import { ref, computed, onMounted } from "vue";
	import axios from "../../config/axios-config";

	export default {
		props: {
			publicationId: Number,
		},
		setup(props) {
			const publication = ref();
			const notFound = ref(false);

			const releaseDate = computed(() => {
				const date = new Date(publication.value.publishedOn);
				const formatedDateString =
					date.getDate() + "." + (date.getMonth() + 1) + "." + date.getFullYear();
				return formatedDateString;
			});

			const handleFetchDetails = () => {
				if (props.publicationId) {
					axios
						.get(`/publication/get?id=${props.publicationId}`)
						.then((response) => {
							publication.value = response.data;
						})
						.catch(() => {
							notFound.value = true;
						});
				}
			};

			onMounted(handleFetchDetails);

			return {
				publication,
				releaseDate,
				notFound,
			};
		},
	};
</script>

<style scoped>
	.details-wrapper {
		display: flex;
		flex-direction: row;
		box-shadow: 0 0 5px 3px rgba(0, 0, 0, 0.5);
		padding: 20px;
		margin: 50px 100px;
		border-radius: 10px;
	}
	.image-container {
		min-width: 300px;
		width: 300px;
		height: 450px;
		overflow: hidden;
	}

	.image-container img {
		width: 100%;
		height: 100%;
		border-radius: 10px;
	}

	.details-container {
		text-align: left;
		padding-left: 30px;
	}

	.details-container .title {
		font-size: 35px;
	}

	.details-container .author-name {
		font-size: 24px;
		font-style: italic;
	}

	.description {
		margin-top: 20px;
	}

	.description div {
		font-weight: bold;
		margin-bottom: 5px;
	}

	.release-date {
		margin-top: 15px;
		display: flex;
		flex-direction: row;
	}

	.release-date label {
		margin-right: 5px;
		font-weight: bold;
	}

	.rating {
		display: flex;
		flex-direction: row;
	}
	.rating span {
		margin-right: 5px;
	}
</style>
