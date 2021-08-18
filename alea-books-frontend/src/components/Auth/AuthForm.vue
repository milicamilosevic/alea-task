<template>
	<div class="form-container">
		<form @submit="handleSubmit">
			<div class="title">
				Prijava
			</div>
			<div class="form-control">
				<label for="email">E-mail:</label>
				<input type="email" v-model="email" />
			</div>
			<div class="form-control">
				<label for="password">Lozinka:</label>
				<input type="password" v-model="password" />
			</div>
			<!-- <div class="form-control"> -->
			<button type="submit">Prijavi se</button>
			<!-- </div> -->
		</form>
	</div>
</template>

<script>
	import { ref } from "vue";
	export default {
		setup() {
			const email = ref("");
			const password = ref("");

			const handleSubmit = (event) => {
				event.preventDefault();
				const enteredEmail = email.value.trim();
				const enteredPassword = password.value.trim();
				console.log(enteredEmail, enteredPassword);
			};

			return {
				email,
				password,
				handleSubmit,
			};
		},
	};
</script>

<style scoped>
	.form-container {
		width: 100vw;
		height: 100vh;
		display: flex;
		align-items: center;
		justify-content: center;
	}

	form {
		width: 500px;
		background-color: #90bc71;
		padding: 50px 50px 25px 50px;
		text-align: center;
		border-radius: 25px;
	}

	.title {
		color: white;
		font-size: 24px;
		margin-bottom: 15px;
	}

	.form-control {
		display: flex;
		flex-direction: column;
		padding-bottom: 30px;
		text-align: left;
	}

	.form-control label {
		font-weight: bold;
		color: white;
	}

	.form-control input {
		padding: 7px;
		outline: none;
		border: none;
		font-size: 16px;
		border-radius: 3px;
	}

	button {
		cursor: pointer;
		border-radius: 50px;
		background-color: transparent;
		border: 2px white solid;
		color: white;
		padding: 10px 25px;
		font-size: 16px;
		font-weight: bold;
	}

	button:hover {
		background-color: rgba(0, 0, 0, 0.1);
	}

	button:active {
		box-shadow: 0 0 5px 2px rgba(0, 0, 0, 0.5);
	}
</style>
