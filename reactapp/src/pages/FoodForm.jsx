import  { Component } from 'react';
import axios from 'axios';

class FoodForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            description: '',
            price: 0,
        };
    }

    handleInputChange = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    handleSubmit = async (e) => {
        e.preventDefault();

        const newFood = {
            name: this.state.name,
            description: this.state.description,
            price: this.state.price,
        };

        try {
            // Make a POST request to your API endpoint
            await axios.post('https://localhost:7224/api/food', newFood, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            // Optionally, display a success message or perform navigation after a successful request.
            console.log('Food item created successfully.');
        } catch (error) {
            // Handle errors, such as displaying an error message.
            console.error('Error creating food item:', error);
        }
    }

    render() {
        return (
            <div>
                <h2>Add New Food Item</h2>
                <form onSubmit={this.handleSubmit}>
                    <div>
                        <label>Name:</label>
                        <input type="text" name="name" value={this.state.name} onChange={this.handleInputChange} />
                    </div>
                    <div>
                        <label>Description:</label>
                        <textarea name="description" value={this.state.description} onChange={this.handleInputChange} />
                    </div>
                    <div>
                        <label>Price:</label>
                        <input type="number" name="price" value={this.state.price} onChange={this.handleInputChange} />
                    </div>
                    <div>
                        <button type="submit">Add Food Item</button>
                    </div>
                </form>
            </div>
        );
    }
}

export default FoodForm;
