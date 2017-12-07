function get_trick()
% figure
src=textread('hand_and_thumb.txt');
plot(src(:,1),src(:,2));
hold on
plot(src(:,9),src(:,10));
legend('Hand Right','Wrist Right');
%%
figure

plot3(src(:,3),src(:,4),src(:,5));
hold on
plot3(src(:,6),src(:,7),src(:,8));
legend('Hand Tip','Thumb');
%%
figure
s=size(src);
x=1:s(1);
y=((src(:,3)-src(:,6)).^2+(src(:,4)-src(:,7)).^2+(src(:,5)-src(:,8)).^2).^(1/2);
plot(x,y);
end