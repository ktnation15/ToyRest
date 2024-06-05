const url = 'https://toyrest20240603114826.azurewebsites.net/api/toys';

Vue.createApp({
    data() {
        return {
            Toys: [],
            Toy: {
                id: 0,
                Brand: "",
                Model: "",
                Price: 0
            },
            GetToy: {
                id: 0,
                Brand: "",
                Model: "",
                Price: 0,
            },
            deleteId: 0,

        }
    },
    async created() {
        await this.getAll();
    },
    methods: {
        async getAll() {
            const response = await axios.get(url);
            this.Toys = await response.data;
        },
        async getById(id) {
            const response = await axios.get(url + '/' + id);
            this.GetToy = await response.data;
        },
        async add() {
            await axios.post(url, this.Toy);
            this.getAll();
        },
        async deleteToy(id) {
            const response = await axios.delete(url + '/' + id);
            console.log(response);  // log svar
            this.getAll();
        }
    }
}).mount('#app')