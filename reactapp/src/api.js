import { get } from 'axios';

const apiUrl = 'https://localhost:7224/api/food';

get(apiUrl)
    .then(response => {
        const data = response.data;
        // Process and use the data here
        console.log(data);
    })
    .catch(error => {
        console.error('Error fetching data:', error);
    });
