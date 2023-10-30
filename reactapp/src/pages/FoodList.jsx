import  { Component } from 'react';
import axios from 'axios';

class FoodList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            foodData: [],
            loading: true,
        };
    }

    componentDidMount() {
        // Define the API URL
        const apiUrl = 'https://localhost:7224/api/food';

        // Use Axios to fetch data from the API
        axios.get(apiUrl)
            .then(response => {
                // Update the component's state with the fetched data
                this.setState({
                    foodData: response.data,
                    loading: false,
                });
            })
            .catch(error => {
                console.error('Error fetching data:', error);
                this.setState({ loading: false });
            });
    }

    render() {
        const { foodData, loading } = this.state;

        return (
            <div>
                <h1>Food Menu</h1>
                {loading ? (
                    <p>Loading... Please wait.</p>
                ) : (
                    <ul>
                        {foodData.map(food => (
                            <li key={food.foodId}>
                                <strong>{food.name}</strong> - {food.description} (${food.price})
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        );
    }
}

export default FoodList;
