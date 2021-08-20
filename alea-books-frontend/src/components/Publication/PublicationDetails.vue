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
			<div class="user-rating">
				<label>Vaša ocjena: {{ publication.userRating }} </label>
				<input type="number" v-model="enteredRating" />
				<button @click="submitRating">Ocijeni</button>
			</div>
		</div>
		<div class="rating">
			<span>{{ publication.rating.toFixed(2) }}</span>
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

			const enteredRating = ref();

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

			const submitRating = () => {
				axios
					.post("publication/rate", {
						publicationId: props.publicationId,
						rating: enteredRating.value,
					})
					.then(() => {
						publication.value.userRating = enteredRating.value;
					});
			};

			onMounted(handleFetchDetails);

			return {
				publication,
				releaseDate,
				notFound,
				enteredRating,
				submitRating,
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
	.user-rating input {
		font-size: 16px;
		width: 10%;
		margin-right: 10px;
		outline: none;
		padding: 2px 5px;
		border-radius: 3px;

		text-align: right;
	}
	.user-rating button {
		font-size: 14px;
		padding: 2px 15px;
		background-color: transparent;
		border: 2px #90bc71 solid;
		color: #90bc71;
		border-radius: 30px;
		cursor: pointer;
	}
	.user-rating button:hover {
		background-color: rgba(0, 0, 0, 0.1);
	}
	.user-rating button:active {
		box-shadow: 0 0 5px 3px rgba(0, 0, 0, 0.3);
	}
	.user-rating label {
		margin-right: 10px;
		font-weight: bold;
		padding-top: 5px;
	}
	.user-rating {
		display: flex;
		margin-top: 10%;
		justify-content: flex-end;
	}
</style>
