figure
star=load('cross.txt');
star=star(:,9:10);
%% 调整R参数
for i=1:1000
    P=4e-5*i
    result = [kalman_filter(star(:,1),1e-5,P,star(1,1),1),...
        kalman_filter(star(:,2),1e-5,P,star(2,2),1)];
    title('cross');
    plot(star(:,1),star(:,2));
    hold on
    plot(result(:,1),result(:,2));
    hold off
    legend('源数据','卡尔曼滤波');
    pause
end