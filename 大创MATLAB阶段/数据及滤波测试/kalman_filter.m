%%
% data是一个向量
% Q,R是有关图像平滑度的参数，需要调试
% x0为向量的起始数据
% P0为任意值，不能为零
%%
function X = kalman_filter(data,Q,R,x0,P0)
N = length(data);

K = zeros(N,1);
X = zeros(N,1);
P = zeros(N,1);

X(1) = x0;
P(1) = P0;

for i = 2:N
    K(i) = P(i-1) / (P(i-1) + R);
    X(i) = X(i-1) + K(i) * (data(i) - X(i-1));
    P(i) = P(i-1) - K(i) * P(i-1) + Q;
end