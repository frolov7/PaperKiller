import React from "react";
import LoginPage from "./pages/Authorization";
import UserMenu from "./pages/UserMenu";
import AdminMenu from "./pages/AdminMenu";
import ChangeLinen from "./pages/ChangeLinen";
import Register from "./pages/Registration";
import MyItems from "./pages/MyItems";
import ChangeDataButton from "./pages/ChangeData";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import "./styles/Background.module.css";
import ChangeItems from "./pages/ChangeItems";

function App() {
	return (
		<Router>
			<Routes>
				<Route path="/login" Component={LoginPage} />
				<Route path="/usermenu" Component={UserMenu} /> 
				<Route path="/registration" Component={Register} /> 
				<Route path="/changedata" Component={ChangeDataButton} /> 
				<Route path="/changelinen" Component={ChangeLinen} />
				<Route path="/changeitems" Component={ChangeItems} /> 
				<Route path="/myitems" Component={MyItems} /> 
				<Route path="/adminmenu" Component={AdminMenu} /> 
			</Routes>
		</Router>
	);
}

export default App;
