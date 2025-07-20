import { Form, Input, Button, message } from "antd";
import { useEffect, useState } from "react";
import api from "../services/api";

export default function StudentForm({
    initialValues,
    onSuccess,
    onCancel,
}) {
    const [form] = Form.useForm();
    const [messageApi, contextHolder] = message.useMessage();
    const [isCreateMode, setIsCreateMode] = useState(true);


    useEffect(() => {
        if (initialValues) {
            setIsCreateMode(false);
        }
        if (!isCreateMode) {
            form.setFieldsValue(initialValues);
        }
    }, [isCreateMode,initialValues, form]);

    const handleSubmit = async (values) => {
        try {
            if (isCreateMode) {
                await api.post("/api/v1/students", values);
                messageApi.success("Student created successfully.");
            } else if (!isCreateMode) {
                await api.put(`/api/v1/students/${initialValues.id}`, values);
                messageApi.success("Student updated successfully.");
            }
            form.resetFields();
            if (onSuccess) onSuccess();
        } catch (error) {
            messageApi.error(
                `Failed to ${isCreateMode ? "create" : "update"} student: ${
                    error.message
                }`
            );
        }
    };

    return (
        <>
            {contextHolder}
            <Form
                form={form}
                layout="vertical"
                onFinish={handleSubmit}
                initialValues={initialValues}
            >
                <Form.Item
                    label="Name"
                    name="name"
                    rules={[
                        {
                            required: true,
                            message: "Please input your name!",
                        },
                    ]}
                >
                    <Input placeholder="First Name" />
                </Form.Item>
                <Form.Item
                    label="Email"
                    name="email"
                    rules={[
                        {
                            required: isCreateMode,
                            message: "Please input the email!",
                        },
                        {
                            type: "email",
                            message: "Please enter a valid email!",
                        },
                    ]}
                >
                    <Input placeholder="Email" disabled={!isCreateMode} />
                </Form.Item>
                <Form.Item>
                    <Button
                        type="primary"
                        htmlType="submit"
                        style={{ marginRight: 8 }}
                    >
                        {isCreateMode ? "Create" : "Update"}
                    </Button>
                    <Button
                        onClick={() => {
                            form.resetFields();
                            if (onCancel) onCancel();
                        }}
                    >
                        Cancel
                    </Button>
                </Form.Item>
            </Form>
        </>
    );
}


