<template>
	<div class="publication-item" @click="goToDetails">
		<div class="image-container">
			<img :src="publication.image" :alt="publication.title" />
		</div>
		<div class="title">{{ publication.title }}</div>
		<div class="author">
			{{ `${publication.author.firstName} ${publication.author.lastName}` }}
		</div>
		<div class="rating">
			<span>4.5</span>
			<i class="fa fa-star" aria-hidden="true"></i>
		</div>
	</div>
</template>

<script>
	import { useRouter } from "vue-router";

	export default {
		props: {
			publication: Object,
		},
		setup(props) {
			const router = useRouter();

			const goToDetails = () => {
				console.log("Detalji...");
				router.push(`/details/${props.publication.id}`);
			};

			return {
				goToDetails,
			};
		},
	};
</script>

<style scoped>
	.publication-item {
		border-radius: 5px;
		cursor: pointer;
		display: flex;
		flex-direction: column;
		align-items: center;
		padding: 10px;
		box-shadow: 0 0 5px 3px rgba(0, 0, 0, 0.2);
	}

	.image-container {
		width: 150px;
		height: 230px;
		overflow: hidden;
	}

	.image-container img {
		width: 100%;
		border-radius: 5px;
		background-position: center;
		background-size: cover;
	}

	.title {
		font-weight: bold;
		margin-top: 10px;
		flex-grow: 1;
		display: flex;
		align-items: flex-end;
	}

	.author {
		font-style: italic;
		font-size: 14px;
	}

	.rating {
		display: flex;
		align-items: flex-end;
		justify-content: flex-end;
		width: 100%;
		text-align: right;
		padding-right: 10px;
	}

	.rating span {
		position: relative;
		top: 3px;
		padding-right: 5px;
	}
</style>
