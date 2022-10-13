import {useNavigate} from 'react-router-dom';
export default function Home(){
    const navigate = useNavigate();
    const handleClick = () => {
        navigate('/login');
        }
    const handleClick1 = () => {
        navigate('/register');
        }

        return(
            <div>
                Dashboard
                <button onClick={handleClick}>
                   Login
                </button>
                <button onClick={handleClick1}>
                    Register
                </button>
            </div>
       )
    } 