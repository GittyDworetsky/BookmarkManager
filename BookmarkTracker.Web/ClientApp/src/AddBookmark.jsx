import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";


const AddBookmark = () => {

    const [data, setData] = useState({title: '', url: ''});
    const navigate = useNavigate();

    const onTextChange = e => {
        const copy = { ...data };
        copy[e.target.name] = e.target.value;
        setData(copy);
    }

    const onFormSubmit = async (e) => {
        e.preventDefault();
        console.log(data);
        await axios.post('/api/user/addbookmarkforuser', data )
        navigate('/mybookmarks');

    }

    return (
        <div
            className="row"
            style={{ minHeight: "80vh", display: "flex", alignItems: "center" }}
        >
            <div className="col-md-6 offset-md-3 bg-light p-4 rounded shadow">
                <h3>Add Bookmark</h3>
                <form onSubmit={onFormSubmit}>
                    <input
                        type="text"
                        name="title"
                        placeholder="Title"
                        className="form-control"
                        value={data.title}
                        onChange={onTextChange}
                    />
                    <br />
                    <input
                        type="text"
                        name="url"
                        placeholder="Url"
                        className="form-control"
                        value={data.url}
                        onChange={onTextChange}
                    />
                    <br />
                    <button className="btn btn-primary">Add</button>
                </form>
            </div>
        </div>
    )
}

export default AddBookmark;
