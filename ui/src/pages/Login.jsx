import { Button, Form, Input, message } from "antd";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";

export default function Login() {
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const [messageApi, contextHolder] = message.useMessage();

    const handleLogin = async (values) => {
        try {
            setLoading(true);
            const result = await api.post("/api/Auth/login", values);
            if (result.status === 200) {
                localStorage.setItem("token", result.data.result.message);
                messageApi.success("Login successful!");
                navigate("/students");
            } else {
                messageApi.error("Wrong username or password");
            }
        } catch (error) {
            messageApi.error("Login error:" + error.message);
        } finally {
            setLoading(false);
        }
    };
    return (
        <>
        {contextHolder}
            <Form
                name="Login"
                labelCol={{ span: 8 }}
                wrapperCol={{ span: 16 }}
                style={{ maxWidth: 600 }}
                initialValues={{ remember: true }}
                onFinish={handleLogin}
            >
                <Form.Item
                    label="Email"
                    name="email"
                    initialValue={"user@test.com"}
                    rules={[
                        { required: true, message: "Please input your email!" },
                    ]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Password"
                    name="password"
                    initialValue={"password"}
                    rules={[
                        {
                            required: true,
                            message: "Please input your password!",
                        },
                    ]}
                >
                    <Input.Password />
                </Form.Item>

                <Form.Item label={null}>
                    <Button loading={loading} type="primary" htmlType="submit">
                        Login
                    </Button>
                </Form.Item>
            </Form>
        </>
    );
}
